import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BookService } from '../../services/book.service';
import { CategoryService } from '../../services/category.service';
import { Book, CreateBook, UpdateBook } from '../../models/book.model';
import { Category } from '../../models/book.model';

export interface BookFormData {
  mode: 'add' | 'edit';
  book?: Book;
}

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent implements OnInit {
  bookForm: FormGroup;
  categories: Category[] = [];
  loading = false;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BookFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BookFormData,
    private bookService: BookService,
    private categoryService: CategoryService,
    private snackBar: MatSnackBar
  ) {
    this.isEditMode = data.mode === 'edit';
    this.bookForm = this.createForm();
  }

  ngOnInit(): void {
    this.loadCategories();
    if (this.isEditMode && this.data.book) {
      this.populateForm(this.data.book);
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      author: ['', [Validators.required, Validators.maxLength(200)]],
      isbn: ['', [Validators.maxLength(50)]],
      publishedDate: ['', [Validators.required]],
      quantity: [0, [Validators.required, Validators.min(0)]],
      categoryIds: [[], [Validators.required]]
    });
  }

  populateForm(book: Book): void {
    this.bookForm.patchValue({
      title: book.title,
      author: book.author,
      isbn: book.isbn,
      publishedDate: new Date(book.publishedDate),
      quantity: book.quantity,
      categoryIds: book.categories.map(c => c.id)
    });
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error loading categories:', error);
        this.snackBar.open('Error loading categories', 'Close', { duration: 3000 });
      }
    });
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      this.loading = true;
      const formValue = this.bookForm.value;

      if (this.isEditMode && this.data.book) {
        const updateBook: UpdateBook = {
          id: this.data.book.id,
          ...formValue
        };

        this.bookService.updateBook(updateBook).subscribe({
          next: () => {
            this.snackBar.open('Book updated successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Error updating book:', error);
            this.snackBar.open('Error updating book', 'Close', { duration: 3000 });
            this.loading = false;
          }
        });
      } else {
        const createBook: CreateBook = {
          ...formValue
        };

        this.bookService.createBook(createBook).subscribe({
          next: () => {
            this.snackBar.open('Book created successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Error creating book:', error);
            this.snackBar.open('Error creating book', 'Close', { duration: 3000 });
            this.loading = false;
          }
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  getErrorMessage(fieldName: string): string {
    const field = this.bookForm.get(fieldName);
    if (field?.hasError('required')) {
      return `${fieldName} is required`;
    }
    if (field?.hasError('maxlength')) {
      return `${fieldName} is too long`;
    }
    if (field?.hasError('min')) {
      return `${fieldName} must be greater than or equal to 0`;
    }
    return '';
  }
}