using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace MissionPossible.API.Helper
{
    public class EmailTemplateParser
    {
        static IWebHostEnvironment _hostingEnvironment;
        static string sendInvitationToSignInTemplate;
        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static string SendInvitationToSignIn(string firstName , string clientUrl , string stampPassword , string logo)
        {
            if (sendInvitationToSignInTemplate == null)
                sendInvitationToSignInTemplate = ReadPhysicalFile("Templates/InvitationTemplate.html");

            string emailMessage = sendInvitationToSignInTemplate
                .Replace("{first-name}", firstName)
                .Replace("{stamp-pass}" , stampPassword)
                .Replace("{client-side-url}", clientUrl)
                .Replace("{logo}" , logo);

            return emailMessage;
        }
        public static string SendOrderToSupplier(string message)
        {
            if (sendInvitationToSignInTemplate == null)
                sendInvitationToSignInTemplate = ReadPhysicalFile("Templates/SendOrderToSupplierTemplate.html");

            string emailMessage = sendInvitationToSignInTemplate
                .Replace("{msg}", message);


            return emailMessage;
        }

        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(EmailTemplateParser)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found ");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
