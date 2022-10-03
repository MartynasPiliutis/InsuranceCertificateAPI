import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  templateUrl: './new-certificate.component.html',
})
export class NewCertificateComponent implements OnInit {

  form: FormGroup;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.form = this.fb.group({
      customerName: ['', [Validators.required]],
      customerDateOfBirth: ['', [Validators.required]],
      insuredItem: ['', [Validators.required]],
      insuredSum: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    const { valid, value } = this.form;

    if (valid) {
      this.http.post(this.baseUrl + 'certificates', value).subscribe(result => {
        this.router.navigateByUrl('/');
      }, error => console.error(error));
    }
  }
}
