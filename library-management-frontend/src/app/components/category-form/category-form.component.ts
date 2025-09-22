import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CategoryService, CreateCategory, UpdateCategory } from '../../services/category.service';
import { Category } from '../../models/book.model';

export interface CategoryFormData {
  mode: 'add' | 'edit';
  category?: Category;
}

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss']
})
export class CategoryFormComponent implements OnInit {
  categoryForm: FormGroup;
  loading = false;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CategoryFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CategoryFormData,
    private categoryService: CategoryService,
    private snackBar: MatSnackBar
  ) {
    this.isEditMode = data.mode === 'edit';
    this.categoryForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.isEditMode && this.data.category) {
      this.populateForm(this.data.category);
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(500)]]
    });
  }

  populateForm(category: Category): void {
    this.categoryForm.patchValue({
      name: category.name,
      description: category.description
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      this.loading = true;
      const formValue = this.categoryForm.value;

      if (this.isEditMode && this.data.category) {
        const updateCategory: UpdateCategory = {
          id: this.data.category.id,
          ...formValue
        };

        this.categoryService.updateCategory(updateCategory).subscribe({
          next: () => {
            this.snackBar.open('Category updated successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Error updating category:', error);
            this.snackBar.open('Error updating category', 'Close', { duration: 3000 });
            this.loading = false;
          }
        });
      } else {
        const createCategory: CreateCategory = {
          ...formValue
        };

        this.categoryService.createCategory(createCategory).subscribe({
          next: () => {
            this.snackBar.open('Category created successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Error creating category:', error);
            this.snackBar.open('Error creating category', 'Close', { duration: 3000 });
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
    const field = this.categoryForm.get(fieldName);
    if (field?.hasError('required')) {
      return `${fieldName} is required`;
    }
    if (field?.hasError('maxlength')) {
      return `${fieldName} is too long`;
    }
    return '';
  }
}