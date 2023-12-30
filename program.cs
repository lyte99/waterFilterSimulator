using System;
using System.Text.RegularExpressions;
using System.Threading;

class WaterFiltrationSystem
{
    const double MAX_IMPURITY_LEVEL = 50;
    const double MAX_PH_LEVEL = 8.5;
    const double MIN_PH_LEVEL = 6.5;
    const double MAX_TEMPERATURE = 100;

    static double waterFlowRate = 70;
    static double impurityLevel  =25;
    static double waterPH = 7;
    static double waterTemp = 75;

    static Random random = new Random();

    static void Main(string[] args)
    {
        while (true)
        {
            UpdateSimulationValues();

            Console.WriteLine($"Current Water Flow: {Math.Round(waterFlowRate,2)} L/min, Impurity Level: {Math.Round(impurityLevel,2)} ppm, pH: {Math.Round(waterPH,2)}, Temperature: {Math.Round(waterTemp, 2)}°C");

            double filteredWaterQuality = CalculateFilteredWaterQuality(ref waterFlowRate, impurityLevel, waterPH, waterTemp);

            Console.WriteLine($"Filtered Water Quality: {Math.Round(filteredWaterQuality,2)}\n");

            Thread.Sleep(1000); // Wait for 1 second (simulating time passage)
        }
    }

    static void UpdateSimulationValues()
    {
        // Updating values in a controlled manner
        waterFlowRate = UpdateValue(waterFlowRate, 5, 75, 5,true);
        impurityLevel = UpdateValue(impurityLevel, 0, 100,1, true);
        waterPH = UpdateValue(waterPH, MIN_PH_LEVEL,MAX_PH_LEVEL,0.5,false);
        waterTemp = UpdateValue(waterTemp, 10, 30,0.5, false);
    }


    static double UpdateValue(double currentValue, double min, double max, double range, bool largeNumber)
    {
        // Generate a random change between -0.5 and 0.5
        double change = (double)(random.NextDouble() - range);

        if(currentValue >= max || currentValue <= min)
        {
            change = -change;
        }

        if (largeNumber)
        {
            currentValue += change +1;
        }
        else
        {
            currentValue += change;
        }
        

        // Ensure the value stays within the min and max bounds
        return Math.Clamp(currentValue, min, max);


    }


    static double CalculateFilteredWaterQuality(ref double waterFlowRate, double impurityLevel, double waterPH, double waterTemp)
    {
        bool isFlowReduced = false;

        //double quality = 100 - (impurityLevel + waterPH + waterTemp - waterFlowRate);
        double quality = 100 - (impurityLevel + waterPH + waterTemp) + (100 - waterFlowRate);

        quality = Math.Clamp(quality, 0, 100);

        //if (impurityLevel > MAX_IMPURITY_LEVEL || waterPH > MAX_PH_LEVEL || waterTemp > MAX_TEMPERATURE)
        if (quality < 90)
        {
            Console.WriteLine("Warning: Input levels too high. Reducing water flow rate to maintain quality.");
            waterFlowRate = Math.Max(1, waterFlowRate / 2);
            isFlowReduced = true;
        }



        if (isFlowReduced)
        {
            Console.WriteLine($"Adjusted Water Flow Rate: {waterFlowRate} L/min");
        }
        else
        {
            isFlowReduced = false;
        }

        return quality;
    }
}
