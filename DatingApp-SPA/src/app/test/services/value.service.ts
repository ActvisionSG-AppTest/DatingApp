import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class ValueService {
  constructor(private http: HttpClient) {}

  getValues() {
    return this.http
      .get("http://localhost:5000/api/values")
      .pipe(map(res => res));
  }
}
