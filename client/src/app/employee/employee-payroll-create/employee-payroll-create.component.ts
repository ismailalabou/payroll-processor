import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { EmployeeDetail, EmployeePayrollCreate } from '@employee/employee-detail/state/employee-detail.model';
import { EmployeeDetailService } from '@employee/employee-detail/state/employee-detail.service';

@Component({
  selector: 'app-employee-payroll-create',
  templateUrl: './employee-payroll-create.component.html',
  styleUrls: ['./employee-payroll-create.component.scss'],
})
export class EmployeePayrollCreateComponent implements OnChanges {
  filterForm = new FormGroup({
    checkDate: new FormControl(''),
    employeeId: new FormControl(''),
    grossPayroll: new FormControl(''),
  });

  @Input()
  set employee(employee: EmployeeDetail) {
    this.filterForm.patchValue({
      employeeId: employee.id,
    });
  }

  constructor(private detailService: EmployeeDetailService) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.filterForm.reset();
  }

  create(): void {
    const payroll: EmployeePayrollCreate = {
      checkDate: this.filterForm.get('checkDate').value,
      employeeId: this.filterForm.get('employeeId').value,
      grossPayroll: this.filterForm.get('grossPayroll').value,
    };

    this.detailService.createPayroll(payroll);
  }
}