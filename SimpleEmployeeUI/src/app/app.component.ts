import { Component, OnInit } from '@angular/core';
import { Employees } from './models/employees';
import { EmployeeService } from './services/employee.service';
import { NgIf, NgForOf, CommonModule } from '@angular/common';
import { Form, FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [NgIf, NgForOf, FormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  employees: Employees[] = [];
  errorMessage!: string;
  successMessage = '';

  editEmployeeId: number | null = null;

  newEmployee = {
    name: '',
    email: '',
    department: '',
    salary: 0,
  };

  constructor(private empService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  startEdit(employee: Employees) {
    this.editEmployeeId = employee.id;

    this.newEmployee = {
      name: employee.name,
      email: employee.email,
      department: employee.department,
      salary: employee.salary,
    };

    this.successMessage = '';
    this.errorMessage = '';
  }

  deleteEmployee(id: number) {
    if (!confirm('Are you sure you want to delete this employee?')) {
      return;
    }

    this.empService.deleteEmployee(id).subscribe({
      next: () => {
        this.successMessage = 'Employee deleted successfully...';
        this.errorMessage = '';
        this.loadEmployees();

        setTimeout(() => {
          this.successMessage = '';
        },2000);
      },
      error: (error) => {
        this.errorMessage = 'Unable to delete employee.';
      },
    });
  }

  addEmployee(form : NgForm) {
    this.successMessage = '';
    this.errorMessage = '';

    if (form.invalid) {
      return;
    }

    if (!this.editEmployeeId) {
      this.empService.createEmployee(this.newEmployee).subscribe({
        next: () => {
          this.successMessage = 'Employee created successfully...!';
          form.resetForm();

          this.loadEmployees();
          this.newEmployee = {
            name: '',
            email: '',
            department: '',
            salary: 0,
          };

          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (error) => {
          console.log(error);
          this.errorMessage = 'Unable to add employee...';
        },
      });
    } else {
      this.empService
        .updateEmployee(this.editEmployeeId, this.newEmployee)
        .subscribe({
          next: () => {
            this.successMessage = 'Employee updated successfully...';
            form.resetForm();
            this.loadEmployees();

            this.newEmployee = {
              name: '',
              email: '',
              department: '',
              salary: 0,
            };

            this.editEmployeeId = null;

            setTimeout(() => {
              this.successMessage = '';
            }, 3000);
          },
          error: (err) => {
            console.log(err);
            this.errorMessage = 'Unable to update employee.';
          },
        });
    }
  }

  loadEmployees() {
    this.empService.getAllEmployees().subscribe({
      next: (data) => {
        this.employees = data;
        console.log('All employee data : ', this.employees);
      },
      error: (error) => {
        this.errorMessage =
          'Unable to load employees. Check that the API is running and the URL is correct.';
      },
    });
  }
}
