export interface Employee {
  id: string;
  department: string;
  employmentStartedOn: string;
  firstName: string;
  lastName: string;
  phone: string;
  status: Status;
  title: string;
}

export type Status = 'ACTIVE' | 'DISABLED';