using Hexagon.RoboSim.Models.Config;
using Hexagon.RoboSim.Models.ModelFactories;
using Hexagon.RoboSim.Movement.Engine;
using Hexagon.RoboSim.Services;
using Hexagon.RoboSim.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Hexagon.RoboSim.Console
{
    public class MainApp
    {
        private IMovementService<int,int> _service;
        private readonly ILogger _logger;
        private readonly RoboCommandSource _roboCommandSource;
        private readonly MoveArea _moveArea;
        public MainApp(ILogger<MainApp> logger, IOptions<MoveArea> moveArea, IOptions<RoboCommandSource> roboCommandSource)
        {
            _logger             = logger;
            _roboCommandSource  = roboCommandSource.Value;
            _moveArea           = moveArea.Value;

            System.Console.OutputEncoding = Encoding.ASCII;
        }

        private void BuildMovementService(MoveArea moveArea)
        {
            var shape           = ShapeFactory<int>.Create(moveArea.Shape);
            var validHeight     = int.TryParse(moveArea.Dimension.Height, out int height);
            var validWidth      = int.TryParse(moveArea.Dimension.Width, out int width);
            var validMoveStep   = int.TryParse(moveArea.MoveStep, out int moveStep);

            if (!validHeight || !validWidth || !validMoveStep || height == 0 || width == 0 || moveStep ==0)
                throw new ArgumentException("Invalid dimension for the move area");

            shape.Height    = height;
            shape.Width     = width;


            _service = new MovementService<int, int>
                           (
                                new Robo<int, int>(shape)
                                {
                                    Step = moveStep
                                }
                           );

        }
        public void Run()
        {
            bool success ;
            do
            {
                System.Console.WriteLine("Choose an option");
                System.Console.WriteLine("----------------");
                System.Console.WriteLine("1.On Screen Commands");
                System.Console.WriteLine("2.Process Commands in File [Ensure file name configured in appsettings.json]");
                System.Console.WriteLine("3.Exit");
                var strInput = System.Console.ReadLine();

                success = int.TryParse(strInput, out int input);

                switch(input)
                {
                    case 1:
                        BuildMovementService(_moveArea);
                        success = ProcessOnScreenCommands();
                        break;
                    case 2:
                        BuildMovementService(_moveArea);
                        success = ProcessFile(_roboCommandSource.Path);
                        break;
                    case 3:
                        success = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid input, please try again");
                        break;
                }

            }while (success);
        }

        private bool ProcessOnScreenCommands()
        {
            while (true)
            {
                System.Console.WriteLine("Please enter a command for Robot [Bye to exit] :");
                var command = System.Console.ReadLine();
                if (command.Equals("Bye", StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.WriteLine(System.Environment.NewLine);
                    return true;
                }
                try
                {
                    var result = _service.ProcessCommand(command);
                    if (!string.IsNullOrEmpty(result))
                        System.Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }

        private bool ProcessFile(string fileName)
        {
            try
            {
                var commands = File.ReadAllLines(fileName).ToList();

                foreach (var command in commands)
                {
                    System.Console.WriteLine($"Performing :{command}");
                    var result = _service.ProcessCommand(command);
                    if (!string.IsNullOrEmpty(result))
                        System.Console.WriteLine(result);
                }
                System.Console.WriteLine(System.Environment.NewLine);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
