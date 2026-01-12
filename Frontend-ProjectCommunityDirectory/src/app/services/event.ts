import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Event } from '../models/event.model';
import { environment } from '../../environments/environment.development';
import { EventCreate } from '../models/event-create.model';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private url = `${environment.apiUrl}/Events`;

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Event[]>(this.url);
  }

  getById(id: number) {
    return this.http.get<Event>(`${this.url}/${id}`);
  }

  create(data: EventCreate) {
    return this.http.post(this.url, data); // Authenticated
  }

  delete(id: number) {
    return this.http.delete(`${this.url}/${id}`); // Admin
  }

  getFullImageUrl(imagePath: string): string {
    if (!imagePath) return 'assets/images/event-placeholder.jpg';

    if (imagePath.startsWith('http')) {
      return imagePath;
    }

    // // Check if it's already a full path
    // if (imagePath.startsWith('/')) {
    //   return `${environment.apiUrl}${imagePath}`;
    // }
    // return `${environment.apiUrl}/imgs/${imagePath}`;
    const baseUrl = environment.apiUrl.replace('/api', '');
    return `${baseUrl}${imagePath}`;
  }
}
