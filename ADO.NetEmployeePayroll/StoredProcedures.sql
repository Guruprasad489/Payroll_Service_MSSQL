-----Add Employee to database-----
create procedure spAddEmployee
(
@Name varchar(100),
@Salary float,
@Startdate Date,
@Gender char(1),
@Phone bigint,
@Department varchar(100),
@Address varchar(100),
@Basic_Pay float,
@Deductions float,
@Taxable_pay float,
@Income_tax float,
@Net_pay float,
@Dept_id int
)
as
BEGIN TRY
	insert into employee_payroll (Name, Salary, Startdate, Gender, Phone, Address, Department, Basic_Pay, Deductions, Taxable_pay, Income_tax, Net_pay)
	values(@Name, @Salary, @Startdate, @Gender, @Phone, @Address, @Department, @Basic_Pay, @Deductions, @Taxable_pay, @Income_tax, @Net_pay)
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH
exec spAddEmployee;

--Update employee details in the database--
create procedure spUpdateEmployee
(
@Id int,
@Name varchar(100),
@Salary float,
@Basic_Pay float
)
as
BEGIN TRY
	update employee_payroll set Salary = @Salary where Id = @Id and Name = @Name;
	update employee_payroll set Basic_Pay = @Basic_Pay where Id = @Id and Name = @Name;
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH