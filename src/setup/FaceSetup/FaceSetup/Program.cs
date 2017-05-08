using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;

namespace FaceSetup
{
    class Program
    {
        static string apiKey = System.Configuration.ConfigurationManager.AppSettings["apiKey"];
        static string personGroupId = "civicans";

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {           
            var faceServiceClient = new FaceServiceClient(apiKey, "https://westeurope.api.cognitive.microsoft.com/face/v1.0");

            try
            {
                //await CreateGroup(faceServiceClient);

                //await LoadPics(faceServiceClient, @"C:\path\to\pics");

                await TestPicture(faceServiceClient, @"C:\path\to\testpic.jpg");
            }
            catch (FaceAPIException ex)
            {
                Console.WriteLine(ex.ErrorCode);
                Console.WriteLine(ex.ErrorMessage);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        private static async Task LoadPics(FaceServiceClient faceServiceClient, string directory)
        {
            var pics = Directory.EnumerateFiles(directory);

            foreach (var pic in pics)
            {
                var name = pic.Substring(pic.LastIndexOf('\\') + 1, pic.IndexOf('.') - pic.LastIndexOf('\\') - 1).Replace('_', ' ');
                Console.WriteLine($"Found {name}");

                var person = await faceServiceClient.CreatePersonAsync(
                    // Id of the person group that the person belonged to
                    personGroupId,
                    // Name of the person
                    name
                );

                using (Stream s = File.OpenRead(pic))
                {
                    // Detect faces in the image and add to Anna
                    await faceServiceClient.AddPersonFaceAsync(
                        personGroupId, person.PersonId, s);
                }
            }

            Console.WriteLine("Training...");
            await faceServiceClient.TrainPersonGroupAsync(personGroupId);

            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);

                if (trainingStatus.Status != Status.Running)
                {
                    Console.WriteLine($"Status: {trainingStatus.Status}");
                    break;
                }

                await Task.Delay(1000);
            }

            Console.WriteLine("Good to go?");
        }

        private static async Task TestPicture(FaceServiceClient faceServiceClient, string testImageFile)
        {
            using (Stream s = File.OpenRead(testImageFile))
            {
                var faces = await faceServiceClient.DetectAsync(s);
                var faceIds = faces.Select(face => face.FaceId).ToArray();

                if (!faceIds.Any())
                {
                    Console.WriteLine("No faces found");
                }
                else
                {
                    var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                    foreach (var identifyResult in results)
                    {
                        Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                        if (identifyResult.Candidates.Length == 0)
                        {
                            Console.WriteLine("No one identified");
                        }
                        else
                        {
                            // Get top 1 among all candidates returned
                            var candidateId = identifyResult.Candidates[0].PersonId;
                            var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                            Console.WriteLine("Identified as {0}", person.Name);
                        }
                    }
                }
            }
        }

        private static async Task CreateGroup(FaceServiceClient faceServiceClient)
        {
            await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Civica Staff");
            Console.WriteLine("Created group!");
        }
    }
}
