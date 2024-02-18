using AutoMapper;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;

namespace GeneralNotes.Application.UseCases.Dashboard;
public class DashboardUseCase(IWorkoutRoutineReadOnlyRepository readOnlyRepository, ILoggedUser logged, IMapper mapper) : IDashboardUseCase
{
    private readonly IWorkoutRoutineReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly ILoggedUser _logged = logged;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseDashboardJson> GetDashboard(RequestDashboardJson requestDashboard)
    {
        var loggedUser = await _logged.RecoverUser();

        var routines = await _readOnlyRepository.GetRoutinesByUser(loggedUser.Id);

        return new ResponseDashboardJson
        {
            WorkoutRoutines = _mapper.Map<List<ResponseRoutinesDashboardJson>>(routines)
        };
    }
}
