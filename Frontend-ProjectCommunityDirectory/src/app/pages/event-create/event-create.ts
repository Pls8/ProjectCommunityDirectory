import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category';
import { EventService } from '../../services/event';
import { Category } from '../../models/category.model';
import { Router } from '@angular/router';
import { EventCreate } from '../../models/event-create.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-event-create',
  imports: [FormsModule],
  standalone: true,
  templateUrl: './event-create.html',
  styleUrl: './event-create.css',
})

export class EventCreateComponent implements OnInit {
  categories: Category[] = [];

  model: EventCreate  = {
    title: '',
    description: '',
    eventDate: '',
    location: '',
    organizer: '',
    imagePath: '',
    categoryId: 0
  };

  constructor(
    private eventService: EventService,
    private categoryService: CategoryService,
    private router: Router
  ) { }

  ngOnInit() {
    this.categoryService.getAll().subscribe(res => this.categories = res);
  }

  submit() {
    this.eventService.create(this.model).subscribe(() => {
      this.router.navigateByUrl('/events');
    });
  }

}