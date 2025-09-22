export interface Book {
  id: number;
  title: string;
  author: string;
  isbn?: string;
  publishedDate: Date;
  quantity: number;
  createdDate: Date;
  updatedDate?: Date;
  categories: Category[];
}

export interface CreateBook {
  title: string;
  author: string;
  isbn?: string;
  publishedDate: Date;
  quantity: number;
  categoryIds: number[];
}

export interface UpdateBook {
  id: number;
  title: string;
  author: string;
  isbn?: string;
  publishedDate: Date;
  quantity: number;
  categoryIds: number[];
}

export interface Category {
  id: number;
  name: string;
  description?: string;
  createdDate: Date;
  updatedDate?: Date;
  books?: Book[];
}