using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Raven.Client.Document;
using quiz.web.Models;

namespace quiz.tests
{
    [TestFixture]
    public class SetupTests
    {
        private DocumentStore docStore;

        [Test]
        public void Initialize()
        {
            docStore = new DocumentStore() {ConnectionStringName = "RavenDB"};
            docStore.Initialize();

            using (var docSession = docStore.OpenSession())
            {
                var questionList = QuestionList.New();

                var questions  = new List<Question>()
                            {
                                new Question()
                                    {
                                        QuestionTitle =
                                            "What is the most common issue category on requests received by Universum Helpdesk this year?",
                                        Options = new List<string>(){ "Outlook","VPN","Onboarding" },
                                        CorrectOption = 0
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "Who has the largest mailbox on our email server except Lars-Henrik? (to keep this even)",
                                        Options = new List<string>(){ "Karl-Johan Hasselström","Martin Strömqvist","Jörgen Gullbrandson" },
                                        CorrectOption = 1
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "How many systems are maintained by Universum Operations?",
                                        Options = new List<string>(){ "22","36","30" },
                                        CorrectOption = 1
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "How many requests have been submitted to Universum Helpdesk YTD?",
                                        Options = new List<string>(){ "1891","948","1652" },
                                        CorrectOption = 0
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "How much RAM (Memory) does all Universum laptops have together?",
                                        Options = new List<string>(){ "710 GB","560 GB","810 GB" },
                                        CorrectOption = 0
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "How many actual server machines do we use to run our internal systems?",
                                        Options = new List<string>(){ "3","10","16" },
                                        CorrectOption = 0
                                    },
                                    new Question()
                                    {
                                        QuestionTitle =
                                            "How many potential issues with internal services are reported to Operations through automatic monitoring on an average month?",
                                        Options = new List<string>(){ "397","345","129" },
                                        CorrectOption = 1
                                    }
                    };

                questionList.Questions = questions;

                docSession.Store(questionList);
                docSession.SaveChanges();
            }

        }
    }
}
