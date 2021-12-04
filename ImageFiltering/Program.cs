using System;
using System.Diagnostics;
using ImageFiltering.Service;

namespace ImageFiltering
{
    class Program
    {
        static void Main(string[] args)
        {
            IImageLoader loader = new ImageLoader();
            
            Console.Write("Enter image path: ");
            string path = Console.ReadLine();
            
            bool isValidFilterSize = false;
            int filterSize = 0;

            while (!isValidFilterSize)
            {
                Console.Write("Enter filter size: ");
                string userFilterInput = Console.ReadLine();
                if (userFilterInput.ToLower() == "exit") Environment.Exit(0);

                isValidFilterSize = int.TryParse(userFilterInput, out filterSize);
                if (!isValidFilterSize) Console.WriteLine("Filter size must be an integer. Try again.");
                if (!isValidFilterSize) Console.WriteLine("Type \"exit\" to terminate the program");
            }

            Console.Write("Enter save location: ");
            string savePath = Console.ReadLine();

            var imageManipulations = loader.LoadImage(path);
            var filter = imageManipulations.GetFilter(Service.Filters.FilterEnum.MedianFilter, filterSize);

            var filteredImage = filter.Apply();
            loader.SaveImage(savePath, filteredImage);
        }
    }
}
