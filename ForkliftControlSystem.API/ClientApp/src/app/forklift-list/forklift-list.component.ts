import { Component, OnInit } from '@angular/core';
import { ForkliftService } from './forklift.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';
import { NzTableModule } from 'ng-zorro-antd/table';

interface ForkliftDTO {
  name: string;
  modelNumber: string;
  manufacturingDate: string;
  age: number;
}

@Component({
  selector: 'app-forklift-list',
  standalone: true,
  imports: [
    NzUploadModule,
    NzTableModule,
    CommonModule
  ],
  templateUrl: './forklift-list.component.html',
  styleUrl: './forklift-list.component.css'
})
export class ForkliftListComponent implements OnInit {
  fileList: any[] = [];
  importSuccess = false;
  importError= false;
  forklifts: ForkliftDTO[] = []; 
  newForklift: Partial<ForkliftDTO> = { name: '', modelNumber: '', manufacturingDate: '' };
  message: any;

  constructor(private forkliftService : ForkliftService) {}

  ngOnInit() {
    this.loadForklifts();
  }

  loadForklifts() {
    this.forkliftService.getForklifts().subscribe((data: any[])  => {
      this.forklifts = data;
    }, (error) => {
      this.message.error('Failed to fetch forklifts');
    });
  }

  beforeUpload = (file: NzUploadFile): boolean => {
    const actualFile: File = file as any; 

    this.forkliftService.uploadForklifts(actualFile).subscribe({
      next: (response) => {
        this.importError = false;
        this.importSuccess= true;
        this.message = 'File imported successfully!';
        this.forklifts = response; 
        this.loadForklifts(); 
      },
      error: (error) => {
        this.importError = true;
        this.importSuccess= false;
        this.message = error.error;
      }
    });
    return false; 
  };
}
