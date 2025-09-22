import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CategoryService, CreateCategory, UpdateCategory } from '../../services/category.service';
import { Category } from '../../models/book.model';
import { CategoryFormComponent } from '../category-form/category-form.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'bookCount', 'actions'];
  dataSource = new MatTableDataSource<Category>();
  categories: Category[] = [];
  loading = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private categoryService: CategoryService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadCategories(): void {
    this.loading = true;
    this.categoryService.getAllCategoriesWithBooks().subscribe({
      next: (categories) => {
        this.categories = categories;
        this.dataSource.data = categories;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading categories:', error);
        this.snackBar.open('Error loading categories', 'Close', { duration: 3000 });
        this.loading = false;
      }
    });
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(CategoryFormComponent, {
      width: '500px',
      data: { mode: 'add' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCategories();
      }
    });
  }

  openEditDialog(category: Category): void {
    const dialogRef = this.dialog.open(CategoryFormComponent, {
      width: '500px',
      data: { mode: 'edit', category: category }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCategories();
      }
    });
  }

  deleteCategory(category: Category): void {
    if (confirm(`Are you sure you want to delete "${category.name}"?`)) {
      this.categoryService.deleteCategory(category.id).subscribe({
        next: () => {
          this.snackBar.open('Category deleted successfully', 'Close', { duration: 3000 });
          this.loadCategories();
        },
        error: (error) => {
          console.error('Error deleting category:', error);
          this.snackBar.open('Error deleting category', 'Close', { duration: 3000 });
        }
      });
    }
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}