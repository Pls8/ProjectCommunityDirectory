import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Resource } from '../models/resource.model';
import { environment } from '../../environments/environment.development';
import { ResourceCreate } from '../models/resource-create.model';

@Injectable({
  providedIn: 'root',
})
export class ResourceService {

  private baseUrl = `${environment.apiUrl}/Resources`;

  constructor(private http: HttpClient) { }

  // getAll() { //oldcode
  //   return this.http.get<Resource[]>(this.baseUrl);
  // }
  getAll(): Observable<Resource[]> {
    return this.http.get<Resource[]>(this.baseUrl);
  }

  // getById(id: number) { //oldcode
  //   return this.http.get<Resource>(`${this.baseUrl}/${id}`);
  // }
  getById(id: number): Observable<Resource> {
    return this.http.get<Resource>(`${this.baseUrl}/${id}`);
  }

  // search(query: string) { //oldcode
  //   return this.http.get<Resource[]>(`${this.baseUrl}/search?query=${query}`);
  // }
  //client-side search v1
  //   search(query: string): Observable<Resource[]> {
  //   // If backend has search endpoint, use it:
  //   // return this.http.get<Resource[]>(`${this.baseUrl}/search?q=${query}`);

  //   // Otherwise, implement client-side search
  //   return this.http.get<Resource[]>(this.baseUrl).pipe(
  //     map(resources => resources.filter(resource => 
  //       resource.name.toLowerCase().includes(query.toLowerCase()) ||
  //       resource.description.toLowerCase().includes(query.toLowerCase()) ||
  //       resource.city.toLowerCase().includes(query.toLowerCase())
  //     ))
  //   );
  // }

  //client-side search v2
  //  search(query: string): Observable<Resource[]> {
  //   return this.http.get<Resource[]>(`${this.baseUrl}/search?query=${encodeURIComponent(query)}`);
  //   // If your backend doesn't have search endpoint, implement client-side search:
  //   // return this.getAll().pipe(
  //   //   map(resources => resources.filter(r => 
  //   //     r.name.toLowerCase().includes(query.toLowerCase()) ||
  //   //     r.description.toLowerCase().includes(query.toLowerCase()) ||
  //   //     r.city.toLowerCase().includes(query.toLowerCase())
  //   //   ))
  //   // );
  // }
  //client-side search v3
  search(query: string): Observable<Resource[]> {
    const q = query.trim().toLowerCase();
    return this.getAll().pipe(
      map(resources =>
        resources.filter(r =>
          r.name?.toLowerCase().includes(q) ||
          r.description?.toLowerCase().includes(q) ||
          r.city?.toLowerCase().includes(q)
        )
      )
    );
  }




  // create(data: Resource) {
  //   return this.http.post(this.baseUrl, data);
  // }

  // create(data: ResourceCreate) { //oldcode
  //   return this.http.post(this.baseUrl, data);
  // }
  create(data: ResourceCreate): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }


  // approve(id: number) {  //oldcode
  //   return this.http.put(`${this.baseUrl}/approve/${id}`, {});
  // }
  approve(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/approve/${id}`, {});
  }

  // delete(id: number) {   //oldcode
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
