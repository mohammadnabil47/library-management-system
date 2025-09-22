using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesWithBooksAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithBooksAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryWithBooksByIdAsync(id);
            return category != null ? _mapper.Map<CategoryDto>(category) : null;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var createdCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDto>(createdCategory);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(updateCategoryDto.Id);
            if (existingCategory == null)
                throw new ArgumentException("Category not found");

            _mapper.Map(updateCategoryDto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory);
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }
    }
}
