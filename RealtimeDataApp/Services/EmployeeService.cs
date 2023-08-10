using Microsoft.EntityFrameworkCore;
using RealtimeDataApp.Data;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace RealtimeDataApp.Services
{
    public class EmployeeService
    {
        AppDbContext dbContext = new AppDbContext();
        public readonly SqlTableDependency<Employee> _dependency;
        private readonly string _connectioString;
        public EmployeeService()
        {
           _connectioString= "Server=DESKTOP-N02OMRJ\\SQLEXPRESS;Database=CompanyDatabase;Trusted_Connection=True;";
            _dependency = new SqlTableDependency<Employee>(_connectioString, "employee");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            int Value = 0;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employee.AsNoTracking().ToListAsync();
        }

    }
}
