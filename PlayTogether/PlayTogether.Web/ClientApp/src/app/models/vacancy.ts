import { MasterValueItem } from "./master-value-item";

export enum VacancyStatus {
  Active = 0,
  NotActive,
  Closed
}

export class Vacancy {
  id: string;
  title: string;
  status: VacancyStatus;
}

export class VacancyDetail extends Vacancy {
  constructor() {
    super();
    this.vacancyFilter = new VacancyFilter();
  }

  searchFilterId: string;
  description: string;
  vacancyFilter: VacancyFilter;
}

export class VacancyFilter {
  id: string;
  minRating: number;
  minExpirience: number;
  cities: string[];
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];
  workTypes: MasterValueItem[];
}
