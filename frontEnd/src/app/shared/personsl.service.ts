import { Injectable } from '@angular/core';
import { PersonsModel } from './persons-model';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class PersonslService {

  constructor(private http:HttpClient) { }

  readonly url = 'http://localhost:62091/api/Persons';

formData:PersonsModel = new PersonsModel();
list:PersonsModel[];

postPerson(){
  return this.http.post(this.url, this.formData);
}

putPerson(){
  return this.http.put(`${this.url}/${this.formData.personId}`, this.formData);
}

deletePerson(id:number){
  return this.http.delete(`${this.url}/${id}`);
}

refreshList(){
  this.http.get(this.url).toPromise().then(res=>{
    this.list=res as PersonsModel[];
  })
}
}
