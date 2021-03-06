import { Component, OnInit } from '@angular/core';
import { PersonsModel } from '../shared/persons-model';
import { PersonslService } from '../shared/personsl.service';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styles: [
  ]
})
export class PersonsComponent implements OnInit {

  showEdit:boolean=true;
  showDelete:boolean=true;
  constructor( public service:PersonslService) { }

  ngOnInit(): void {

    this.service.refreshList();
  }


  toogleButton(){
    this.showEdit=!this.showEdit;
  }
  toogleButtonDelete(){
    this.showDelete =!this.showDelete;
  }


  populateForm(selectOnePerson:PersonsModel){
    this.service.formData=Object.assign({},selectOnePerson);
    this.service.refreshList();
  }

  onDelete(id:number){
    if(confirm("Are you sure to delete?")){
      this.service.deletePerson(id).subscribe(res=>{this.service.refreshList()}, err=>{console.log(err)});
      
      this.service.refreshList();
    }
  }

}
