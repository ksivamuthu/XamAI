using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;

namespace XamAI.Services.Contracts
{
    public interface IVisionService
    {
        Task<ImageAnalysis> AnalyzeImage(MediaFile mediaFile);

        Task<ImageDescription> DescribeImage(MediaFile mediaFile);

        Task<string> ClassifyImage(MediaFile mediaFile);
    }
}
