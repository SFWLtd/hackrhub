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

namespace HackRHub.Controllers
{
    public class FaceDetectController : ApiController
    {
        static string apiKey = System.Configuration.ConfigurationManager.AppSettings["FaceApiKey"];
        static string personGroupId = "civicans";

        [Route("api/detect")]
        public async Task<string> Detect([FromBody]byte[] image)
        {
            var faceServiceClient = new FaceServiceClient(apiKey, "https://westeurope.api.cognitive.microsoft.com/face/v1.0");

            try
            {
                using (var s = new MemoryStream(image))
                {
                    var faces = await faceServiceClient.DetectAsync(s);
                    var faceIds = faces.Select(face => face.FaceId).ToArray();

                    if (!faceIds.Any())
                    {
                        return "No faces found";
                    }
                    else
                    {
                        var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                        var sb = new StringBuilder();

                        foreach (var identifyResult in results)
                        {
                            if (identifyResult.Candidates.Length == 0)
                            {
                                Console.WriteLine("No one identified");
                            }
                            else
                            {
                                sb.AppendFormat("Result of face: {0}", identifyResult.FaceId);
                                // Get top 1 among all candidates returned
                                var candidateId = identifyResult.Candidates[0].PersonId;
                                var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                                sb.AppendFormat("Identified as {0}", person.Name);
                                sb.AppendLine();
                            }
                        }

                        return sb.ToString();
                    }
                }
            }
            catch (FaceAPIException ex)
            {
                return ex.ErrorMessage;
            }
        }
    }
}
