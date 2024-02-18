using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.Dashboard;
public interface IDashboardUseCase
{
    Task<ResponseDashboardJson> GetDashboard(RequestDashboardJson requestDashboard);
}
