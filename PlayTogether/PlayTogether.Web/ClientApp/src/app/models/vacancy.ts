import { MasterValueItem } from "./master-value-item";

export class Vacancy {
  id: string;
  title: string;
  isClosed: boolean;
}

export class VacancyDetail extends Vacancy {
  constructor() {
    super();
    this.vacancyFilter = new VacancyFilter();
  }

  description: string;
  vacancyFilter: VacancyFilter;
}

export class VacancyFilter {
  id: string;
  vacancyTitle: string;
  minRating: number;
  minExperience: number;
  cities: MasterValueItem[];
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];
  workTypes: MasterValueItem[];
}
