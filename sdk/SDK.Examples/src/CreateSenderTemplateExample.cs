using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CreateSenderTemplateExample : SdkSample
    {
        public PackageId TemplateId;
        public Visibility Visibility = Visibility.SENDER;

        public static void Main(string[] args)
        {
            new CreateSenderTemplateExample().Run();
        }

        override public void Execute()
        {
            var template =
                PackageBuilder.NewPackageNamed("CreateSenderTemplateExample: " + DateTime.Now)
                    .DescribedAs("This is a Template created using the e-SignLive SDK")      
                    .WithVisibility(Visibility)
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                                    .WithFirstName("Patty")    
                                                    .WithLastName("Galant"))   
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .WithId("documentId")  
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                  .OnPage(0)
                                  .AtPosition(400, 200)
                               ))
                    .Build();

            TemplateId = eslClient.CreateTemplate(template);

            Console.WriteLine("templateId = " + TemplateId);
        }

    }
}

