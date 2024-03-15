import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) {

  }

  public Login(email: string, password: string) {
    const body = {
      email: email,
      password: password
    };

    // Define the HTTP headers (optional)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    // Make the POST request
    return this.http.post<any>('http://localhost:5121/api/v1/user/login', body, { headers: headers });
  }

  public Register(email: string, password: string) {
    const body = {
      email: email,
      password: password
    };

    // Define the HTTP headers (optional)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    // Make the POST request
    return this.http.post<any>('http://localhost:5121/api/v1/user/register', body, { headers: headers });
  }

  public GetData() {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.get<any>('http://localhost:5121/api/v1/token/get-data', { headers: headers });
  }

  public RenewToken(token: string, email: string) {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    });

    return this.http.post<any>('http://localhost:5121/api/v1/user/renew-token', {email},{ headers: headers });
  }

  public UpdateData(token: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    });

    return this.http.post<any>('http://localhost:5121/api/v1/token/update-data',null,{ headers: headers });
  }
}
