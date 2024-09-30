using ForkliftControlSystem.Application.DTOs;
using ForkliftControlSystem.Domain.Entities;
using ForkliftControlSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkliftControlSystem.Application.Services
{
    public class ForkliftService : IForkliftService
    {
        private readonly IForkliftRepository _forkliftRepository;

        public ForkliftService(IForkliftRepository forkliftRepository)
        {
            _forkliftRepository = forkliftRepository;
        }

        public async Task<List<ForkliftDTO>> GetAllForklifts(string filePath)
        {
            var forklifts = await _forkliftRepository.GetAllForklifts(filePath);

            var forkliftDTOs = forklifts.Select(f => new ForkliftDTO
            {
                Name = f.Name,
                ModelNumber = f.ModelNumber,
                ManufacturingDate = f.ManufacturingDate,
                Age = CalculateAge(f.ManufacturingDate)
            }).ToList();

            return forkliftDTOs;
        }

        private int CalculateAge(DateTime manufacturingDate)
        {
            var currentYear = DateTime.Now.Year;
            return currentYear - manufacturingDate.Year - (DateTime.Now.DayOfYear < manufacturingDate.DayOfYear ? 1 : 0);
        }


        public List<string> ParseForkliftCommands(string commandInput)
        {
            var actions = new List<string>();

            commandInput = commandInput.Trim().Replace(" ", "");
            var unitString = "";
            var command = default(char);

            foreach (char ch in commandInput.ToCharArray())
            {
                if (char.IsDigit(ch))
                    unitString += ch;
                else
                {
                    if (command == default(char))
                    {
                        command = ch;
                    }
                    else
                    {
                        actions.Add(AddCommand(command, Convert.ToInt64(unitString)));
                        unitString = string.Empty;
                        command = ch;
                    }
                }
            }
            actions.Add(AddCommand(command, Convert.ToInt64(unitString)));
            return actions;
        }
        private string AddCommand(char ch, long units)
        {
            switch (ch)
            {
                case 'F':
                    return ($"Move Forward by {units} meters.");
                case 'B':
                    return ($"Move Backward by {units} meters.");
                case 'L':
                    if (units % 90 != 0)
                        return "Invalid degree for Left movement, Degrees must be a multiple of 90";
                    else return ($"Turn Left by {units} degrees.");
                case 'R':
                    if (units % 90 != 0) return "Invalid degree for Right movement, Degrees must be a multiple of 90";
                    else
                        return ($"Turn Right by {units} degrees.");

                default:
                    return ($"Unknown command: {ch}");

            }
        }
    }
}
