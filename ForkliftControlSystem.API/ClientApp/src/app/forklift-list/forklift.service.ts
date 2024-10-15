import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForkliftService {
  private api = '/forklift';
  constructor(private http: HttpClient) { }

  getForklifts(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl+this.api}`);
  }

  uploadForklifts(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file); // Append the file to the form data

    return this.http.post(`${environment.apiUrl+this.api+"/import"}`, formData); // Your upload endpoint
  }
}
