namespace MovieRating.Web
{
    using System.Web.Http;

    public class Bootstrapper
    {

        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
        }
    }
}