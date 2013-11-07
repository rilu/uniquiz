using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Session;
using Raven.Client;
using quiz.web.Models;

using System.Collections.Generic;

namespace quiz.web.Modules
{
    public class QuizModule : BaseModule
    {
        private readonly IDocumentSession documentSession;
        private const int PageSize = 25;

        public QuizModule(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;

            Post["/api/add"] = _ =>
                {
                    if (Request.Cookies.ContainsKey("quizvote"))
                        return Response.AsRedirect("/highscore", RedirectResponse.RedirectType.Permanent);
                    
                    var questionList = documentSession.Load<QuestionList>("QuestionLists/QuestionList");

                    var submission = this.Bind<Submission>();

                    //Ugly hack due to couldn't get the binding to work correctly, GAH!
                    submission.Answers = new int[questionList.Questions.Count()];
                    for (var i = 1; i <= questionList.Questions.Count(); i++)
                    {
                        if (Request.Form["Answers_" + i] != null && Request.Form["Answers_" + i] != "")
                        {
                            submission.Answers[i-1] = int.Parse(Request.Form["Answers_" + i]);
                        }
                    }

                    var correctScore = questionList.Questions.Select(x => x.CorrectOption).ToList();
                    var score = SubmissionCorrection.Correcting(correctScore,submission.Answers);

                    submission.Score = score;

                    documentSession.Store(submission);
                    documentSession.SaveChanges();

                    return
                        Response.AsRedirect("/highscore", RedirectResponse.RedirectType.Permanent)
                                .AddCookie("quizvote", "true", DateTime.UtcNow.AddDays(365));
                };

            Get["/api/questions"] = _ =>
                {
                    var questionList = documentSession.Load<QuestionList>("QuestionLists/QuestionList");

                    if (questionList == null)
                        return 404;

                    var dictionary =
                        questionList.Questions.ToList().ToDictionary(k => k.QuestionTitle, v => v.Options);

                    var response = dictionary.Select(x => new {question = x.Key, options = x.Value});

                    return Response.AsJson(response);
                };

            Get["/quiz"] = _ =>
            {
                base.Page.Title = "Quiz";

                var questionList = documentSession.Load<QuestionList>("QuestionLists/QuestionList");

                return View["Quiz", questionList];
            };


            Get["/highscore"] = _ =>
                {
                    base.Page.Title = "Highscore";

                    var submissions = documentSession.Query<Submission>().OrderByDescending(x => x.Score).ToList();

                    return View["highscore", submissions];
                };
        } 
    }
}