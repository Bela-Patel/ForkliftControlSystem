import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class ForkliftCommandsService {
  private api = '/forklift';
  constructor(private http: HttpClient) { }

  executeCommands(command: any): Observable<string[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<string[]>(`${environment.apiUrl + this.api + "/executeCommands"}`, JSON.stringify(command), { headers });
  }
}
