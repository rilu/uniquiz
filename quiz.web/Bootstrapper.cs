using Nancy;
using Nancy.TinyIoc;
using Raven.Client;
using Raven.Client.Document;

namespace quiz.web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var docStore = container.Resolve<DocumentStore>("DocStore");
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var store = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            };

            store.Initialize();

            container.Register(store, "DocStore");
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);


            var docStore = container.Resolve<DocumentStore>("DocStore");
            var documentSession = docStore.OpenSession();

            container.Register<IDocumentSession>(documentSession);
        }
    }
}