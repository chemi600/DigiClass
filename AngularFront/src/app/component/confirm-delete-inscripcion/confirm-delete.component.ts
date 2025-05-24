import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-confirm-delete-curso',
  imports:[CommonModule,MatDialogModule,MatButtonModule],
  templateUrl: './confirm-delete.component.html',
})
export class ConfirmDeleteInscripcionComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDeleteInscripcionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: null
  ) {}

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
