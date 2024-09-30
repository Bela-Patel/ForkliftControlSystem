using ForkliftControlSystem.Application.DTOs;

namespace ForkliftControlSystem.Application.Services
{
    public interface IForkliftService
    {
        Task<List<ForkliftDTO>> GetAllForklifts(string filePath);
        List<string> ParseForkliftCommands(string command);

    }
}
