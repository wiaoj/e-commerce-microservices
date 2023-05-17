import { Injectable } from '@angular/core';
import { HttpClientService } from '../common/http-client-service.service';
import { GetRootCategoriesResponse } from 'src/app/common/models/category/GetRootCategoriesResponse';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private httpClientService: HttpClientService) {}

	async getRootCategories(): Promise<GetRootCategoriesResponse> {
		const observable = this.httpClientService.get<GetRootCategoriesResponse>(
			{
				controller: "categories",
				action: "getrootcategories",
			},
			
		);

		return await firstValueFrom(observable);
	}
}
