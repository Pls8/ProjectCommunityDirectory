import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventService } from '../../services/event';
import { AuthService } from '../../services/auth';
import { Event } from '../../models/event.model';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';


@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './events.html',
  styleUrl: './events.css',
})
export class EventsComponent implements OnInit {
  events: Event[] = [];

  events$!: Observable<Event[]>;
  loading = true;
  error = '';


  constructor(
    private eventService: EventService,
    public auth: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents(): void {
    this.loading = true;
    this.error = '';

    this.events$ = this.eventService.getAll().pipe(
      catchError(error => {
        this.error = 'Failed to load events. Please try again.';
        console.error('Error loading events:', error);
        return of([]);
      }),
      finalize(() => {
        this.loading = false;
      })
    );
  }

  delete(id: number) {
    if (!confirm('Delete this event?')) return;
    this.eventService.delete(id).subscribe({
      next: () => {
        this.loadEvents();
      },
      error: (error) => {
        console.error('Error deleting event:', error);
      }
    });
  }
  
  getImageUrl(imagePath: string): string {
    return this.eventService.getFullImageUrl(imagePath);
  }

  

}
