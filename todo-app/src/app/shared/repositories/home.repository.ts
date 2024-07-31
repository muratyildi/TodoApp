import { Injectable, inject } from "@angular/core";
import { HomeService } from "../services/home.service";

@Injectable({
    providedIn: 'root'
})

export class homeRepository {
    homeService = inject(HomeService);

    getTodoDatas() {

    }

    postNewTodo() {

    }

    PutTodo(id: string) {

    }

    deleteTodo(id: string) {

    }

}