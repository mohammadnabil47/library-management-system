import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BookService } from '../../services/book.service';
import { CategoryService } from '../../services/category.service';
import { Book, CreateBook, UpdateBook } from '../../models/book.model';
import { BookFormComponent } from '../book-form/book-form.component';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  displayedColumns: string[] = ['title', 'author', 'isbn', 'publishedDate', 'quantity', 'categories', 'actions'];
  dataSource = new MatTableDataSource<Book>();
  books: Book[] = [];
  loading = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private bookService: BookService,
    private categoryService: CategoryService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadBooks();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadBooks(): void {
    this.loading = true;
    this.bookService.getAllBooksWithCategories().subscribe({
      next: (books) => {
        this.books = books;
        this.dataSource.data = books;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading books:', error);
        this.snackBar.open('Error loading books', 'Close', { duration: 3000 });
        this.loading = false;
      }
    });
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(BookFormComponent, {
      width: '600px',
      data: { mode: 'add' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  openEditDialog(book: Book): void {
    const dialogRef = this.dialog.open(BookFormComponent, {
      width: '600px',
      data: { mode: 'edit', book: book }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  deleteBook(book: Book): void {
    if (confirm(`Are you sure you want to delete "${book.title}"?`)) {
      this.bookService.deleteBook(book.id).subscribe({
        next: () => {
          this.snackBar.open('Book deleted successfully', 'Close', { duration: 3000 });
          this.loadBooks();
        },
        error: (error) => {
          console.error('Error deleting book:', error);
          this.snackBar.open('Error deleting book', 'Close', { duration: 3000 });
        }
      });
    }
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}