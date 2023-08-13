using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeDataApp.Data;
using RealtimeDataApp.Hubs;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace RealtimeDataApp.Services
{
    public class EmployeeService
    {
        private readonly IHubContext<EmployeeHub> _context;
        AppDbContext dbContext = new AppDbContext();
        public readonly SqlTableDependency<Employee> _dependency;
        private readonly string _connectioString;
        public EmployeeService(IHubContext<EmployeeHub> context)
        {
            _context = context; 
           _connectioString= "Server=DESKTOP-N02OMRJ\\SQLEXPRESS;Database=CompanyDatabase;Trusted_Connection=True;";
            _dependency = new SqlTableDependency<Employee>(_connectioString, "Employee");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            var employees = await GetAllEmployees();
            await _context.Clients.All.SendAsync("RefreshEmployees", employees);
            
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employee.AsNoTracking().ToListAsync();
        }

    }
}
