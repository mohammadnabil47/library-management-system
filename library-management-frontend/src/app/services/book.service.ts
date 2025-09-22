import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book, CreateBook, UpdateBook } from '../models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7282/api/books';

  constructor(private http: HttpClient) { }

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getAllBooksWithCategories(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/with-categories`);
  }

  getAllBooksWithCategoriesAdo(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/with-categories-ado`);
  }

  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  getBooksByCategoryId(categoryId: number): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/by-category/${categoryId}`);
  }

  createBook(book: CreateBook): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book);
  }

  updateBook(book: UpdateBook): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${book.id}`, book);
  }

  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}