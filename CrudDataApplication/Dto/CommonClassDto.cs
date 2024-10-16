namespace CrudDataApplication.Dto
{
    public class CommonClassDto
    {
    }
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public bool? IsActive { get; set; }
    }
    public class EmployeesDto
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? HireDate { get; set; }
        public int? AddressId { get; set; }
        public string? AdrressStreet { get; set; }
        public bool? IsActive { get; set; }

    }
    public class CompanyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool? IsActive { get; set; }
    }
    public class DepartmentsDto
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public int? EmployeesId { get; set; }
        public int? ProjectsId { get; set; }
        public string? ManagerName {  get; set; }
        public string? EmployeesName { get; set; }
        public string? ProjectsName { get; set; }
        public bool? IsActive { get; set; }
    }
    public class ManagerDto
    {
        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
    }
    public class ProjectsDto
    {
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Status { get; set; }
        public int? TasksId { get; set; }
        public string? TasksName { get; set; }
        public bool? IsActive { get; set; }
    }
    public class TasksDto
    {
        public int? TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? Deadline { get; set; }
        public bool? Completed { get; set; }
        public bool? IsActive { get; set; }
    }
}
