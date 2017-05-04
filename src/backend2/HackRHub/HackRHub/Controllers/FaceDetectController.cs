using HackRHub.Models;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace HackRHub.Controllers
{
    public class FaceDetectController : ApiController
    {
        static string apiKey = System.Configuration.ConfigurationManager.AppSettings["FaceApiKey"];
        static string personGroupId = "civicans";

        [Route("api/detect")]
        public async Task<Person> Detect([FromBody]string base64Image)
        {
            var faceServiceClient = new FaceServiceClient(apiKey, "https://westeurope.api.cognitive.microsoft.com/face/v1.0");

            byte[] image = Convert.FromBase64String(base64Image);

            try
            {
                using (var s = new MemoryStream(image))
                {
                    var faces = await faceServiceClient.DetectAsync(s);
                    var faceIds = faces.Select(face => face.FaceId).ToArray();

                    if (!faceIds.Any())
                    {
                        throw new InvalidOperationException("No faces detected");
                    }
                    else
                    {
                        var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                        
                        if (results.Length > 1)
                        {
                            throw new InvalidOperationException("Multiple faces detected");
                        }

                        var identifyResult = results.Single();
                        
                        if (identifyResult.Candidates.Length == 0)
                        {
                            throw new InvalidOperationException("No faces identified");
                        }
                        else
                        {
                            // Get top 1 among all candidates returned
                            var candidateId = identifyResult.Candidates[0].PersonId;
                            var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);

                            return new Person
                            {
                                FaceId = identifyResult.FaceId.ToString(),
                                Name = person.Name
                            };
                        }
                    }
                }
            }
            catch (FaceAPIException ex)
            {
                throw;
            }
        }
    }
}
