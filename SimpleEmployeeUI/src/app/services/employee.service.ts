import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employees } from '../models/employees';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private url : string = "https://localhost:7147/api/Employee";

  constructor(private http : HttpClient) { }

  getAllEmployees() : Observable<Employees[]>{
    return this.http.get<Employees[]>(`${this.url}/getAllEmployees`);
  }

  createEmployee(employee : Omit<Employees, 'id'>) : Observable<any>{
    return this.http.post<any>(`${this.url}`, employee);
  }

  updateEmployee(id : number, employee : Omit<Employees, 'id'>) : Observable<any>{
    return this.http.put<any>(`${this.url}/updateEmployee/${id}`, employee);
  }

  deleteEmployee(id : number) : Observable<any>{
    return this.http.delete<any>(`${this.url}/deleteEmployee/${id}`);
  }
}
