import { Component, ElementRef, signal, ViewChild } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { FlexiToastService } from 'flexi-toast';
import { RoleModel } from '../../models/role.model';
import { FlexiGridModule } from 'flexi-grid';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [FlexiGridModule, FormsModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css',
})
export class RolesComponent {
  roles = signal<RoleModel[]>([]);
  name = signal<string>('');
  description = signal<string>('');

  @ViewChild('addModalCloseBtn') addModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  constructor(private http: HttpService, private toast: FlexiToastService) {
    this.getAll();
    console.log(this.roles());
  }

  getAll() {
    this.http.get<RoleModel[]>('Roles/GetAllForClient', (res) => {
      this.roles.set(res);
    });
  }

  deleteByName(name: string) {
    this.toast.showSwal(
      'Delete Role?',
      'Do you want to delete this role?',
      () => {
        this.http.delete<string>(`Roles/DeleteByName/?name=${name}`, (res) => {
          this.toast.showToast('Info', res, 'info');
          this.getAll();
        });
      }
    );
  }

  save() {
    this.http.post<string>(
      'Roles/CreateForClient',
      { name: this.name(), description: this.description() },
      (res) => {
        this.toast.showToast('Info', res, 'info');
        this.name.set('');
        this.getAll();

        this.addModalCloseBtn?.nativeElement.click();
      }
    );
  }
}
