import { Component, OnInit } from '@angular/core';
import { ResourceService } from '../../services/resource';
import { AuthService } from '../../services/auth';
import { Resource } from '../../models/resource.model';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-resources',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './resources.html',
  styleUrl: './resources.css',
})
export class ResourcesComponent implements OnInit {
  resources$!: Observable<Resource[]>;
  hasError = false;

  searchQuery = '';
  private searchSubject = new BehaviorSubject<string>('');

  constructor(
    private resourceService: ResourceService,
    public auth: AuthService,
    private router: Router
  ) { }

  //old code
  // ngOnInit() {
  //   this.loadResources();
  // }
  // loadResources() {
  //   this.hasError = false;
  //   this.resources$ = this.resourceService.getAll().pipe(
  //     catchError(error => {
  //       console.error('Error loading resources:', error);
  //       this.hasError = true;
  //       return of([]); // Return empty array on error
  //     })
  //   );
  // }


  //new code 
  ngOnInit() {
    // Set up search with debounce
    this.resources$ = this.searchSubject.pipe(
      debounceTime(300), // Wait 300ms after typing
      distinctUntilChanged(), // Only search if query changed
      switchMap(query => {
        this.hasError = false;
        if (query.trim()) {
          return this.resourceService.search(query).pipe(
            catchError(error => {
              console.error('Search error:', error);
              this.hasError = true;
              return of([]);
            })
          );
        } else {
          // If no query, get all resources
          return this.resourceService.getAll().pipe(
            catchError(error => {
              console.error('Error loading resources:', error);
              this.hasError = true;
              return of([]);
            })
          );
        }
      })
    );

    // Trigger initial load
    this.loadResources();
  }

  loadResources() {
    this.searchSubject.next(''); // Trigger load with empty query
  }

  onSearch() {
    this.searchSubject.next(this.searchQuery);
  }
  clearSearch() {
    this.searchQuery = '';
    this.searchSubject.next('');
  }
//new code



  delete(id: number) {
    if (!confirm('Delete this resource?')) return;

    this.resourceService.delete(id).subscribe(() => {
      this.loadResources();
    });
  }

  approve(id: number) {
    this.resourceService.approve(id).subscribe(() => {
      this.loadResources();
    });
  }
}