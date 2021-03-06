import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PersonsModel } from 'src/app/shared/persons-model';
import { PersonslService } from 'src/app/shared/personsl.service';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styles: [
  ]
})
export class PersonComponent implements OnInit {

  constructor(public service:PersonslService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  onSubmit(form:NgForm){
    if(this.service.formData.personId==0){
      this.insertRecord(form);
    }
    else{
      this.updateRecord(form);
    }
    
  }

  insertRecord(form:NgForm){
    this.service.postPerson().subscribe(res=>{this.resetForm(form)}, err=>{console.log()})
    this.service.refreshList();
  }
  updateRecord(form:NgForm){
    this.service.putPerson().subscribe(res=>{this.resetForm(form)}, err=>{console.log()})
  }


  onDelete(id:number){
    if(confirm('Are you sure to delete this record?')){
      this.service.deletePerson(id).subscribe(res=>{this.service.refreshList()}, err=>{console.log()});
      
    }
  }


  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData= new PersonsModel();
  }
}
