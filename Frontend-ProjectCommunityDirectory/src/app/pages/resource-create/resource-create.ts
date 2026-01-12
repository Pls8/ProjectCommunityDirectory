import { Component } from '@angular/core';
import { ResourceCreate } from '../../models/resource-create.model';
import { ResourceService } from '../../services/resource';
import { CategoryService } from '../../services/category';
import { Router } from '@angular/router';
import { Category } from '../../models/category.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-resource-create',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './resource-create.html',
  styleUrl: './resource-create.css',
})
export class ResourceCreateComponent {

  // categories: Category[] = [];
  categories$!: Observable<Category[]>;


  model: ResourceCreate = {
    name: '',
    description: '',
    categoryId: 0,
    phone: '',
    contactEmail: '',
    contactInfo: '',
    city: '',
    address: '',
    website: ''
  };

  constructor(
    private resourceService: ResourceService,
    private categoryService: CategoryService,
    private router: Router
  ) { }

  ngOnInit() {
     this.categories$ = this.categoryService.getAll();
    // this.categoryService.getAll().subscribe(res => this.categories = res);
    // this.categoryService.getAll().subscribe({
    //   next: res => {
    //     this.categories = res;
    //     console.log('Categories loaded:', res);
    //   },
    //   error: err => console.error(err)
    // });
  }

  // submit() {
  //   this.resourceService.create(this.model).subscribe(() => {
  //     alert('Resource submitted for approval');
  //     this.router.navigateByUrl('/resources');
  //   });
  // }
  submit() {
    this.resourceService.create(this.model).subscribe({
      next: () => {
        alert('Resource submitted for approval. An admin will review it.');
        this.router.navigateByUrl('/resources');
      },
      error: (error) => {
        console.error('Error creating resource:', error);
        alert('Failed to submit resource. Please try again.');
      }
    });
  }

  resetForm() {
    this.model = {
      name: '',
      description: '',
      categoryId: 0,
      phone: '',
      contactEmail: '',
      contactInfo: '',
      city: '',
      address: '',
      website: ''
    };
  }



}
