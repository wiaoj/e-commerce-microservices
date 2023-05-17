import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
	providedIn: "root",
})
export class HttpClientService {
	private apiUrl: string = environment.apiUrl;
	constructor(private httpClient: HttpClient) {}

	private buildUrl(requestParameter: Partial<RequestParameters>, id?: string): string {
		let url =
			requestParameter.fullEndPoint ||
			`${requestParameter.apiUrl || this.apiUrl}/${requestParameter.controller}${
				requestParameter.action ? `/${requestParameter.action}` : ""
			}`;

		if (id) {
			url += `/${id}`;
		}

		if (requestParameter.queryString) {
			url += `?${requestParameter.queryString}`;
		}

		return url;
	}

	get<Type>(requestParameter: Partial<RequestParameters>, id?: string): Observable<Type> {
		return this.httpClient.get<Type>(this.buildUrl(requestParameter, id), {
			headers: requestParameter.headers,
			responseType: requestParameter.responseType as "json",
		});
	}

	post<Type, TypeResponse>(requestParameter: Partial<RequestParameters>, body: Partial<Type>): Observable<TypeResponse> {
		return this.httpClient.post<TypeResponse>(this.buildUrl(requestParameter), {
			headers: requestParameter.headers,
			responseType: requestParameter.responseType as "json",
		});
	}

	put<Type>(requestParameter: Partial<RequestParameters>, body: Partial<Type>): Observable<Type> {
		return this.httpClient.put<Type>(this.buildUrl(requestParameter), body, {
			headers: requestParameter.headers,
			responseType: requestParameter.responseType as "json",
		});
	}

	delete<Type>(requestParameter: Partial<RequestParameters>, id: string): Observable<Type> {
		return this.httpClient.delete<Type>(this.buildUrl(requestParameter, id), {
			headers: requestParameter.headers,
			responseType: requestParameter.responseType as "json",
		});
	}
}

export class RequestParameters {
	controller?: string;
	action?: string;
	queryString?: string;

	headers?: HttpHeaders;
	apiUrl?: string;
	fullEndPoint?: string; //farklı servislere istek gönderecek kapasiteyi ekliyor

	responseType?: string = "json";
}
