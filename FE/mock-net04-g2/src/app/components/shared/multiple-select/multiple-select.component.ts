import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Organization } from '../../../models/Organization';

@Component({
  selector: 'app-multiple-select',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './multiple-select.component.html',
  styleUrl: './multiple-select.component.scss',
})
export class MultipleSelectComponent implements OnInit, OnChanges {
  @Input() label: string = '';
  @Input() options: Organization[] = [];
  @Output() onOptionSelect = new EventEmitter();

  mapOptions: any[] = [];

  @Input() selectedItems: any[] = [];
  @Output() selectedItemsChange = new EventEmitter<any[]>();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['options'] && !changes['options'].isFirstChange()) {
      this.initializeMapOptions();
    }
    if (changes['selectedItems']) {
      console.log(
        'MultipleSelectComponent - selectedItems changed:',
        this.selectedItems
      );
      this.updateSelectedOptions();
    }
  }

  ngOnInit(): void {
    console.log('MultipleSelectComponent - ngOnInit - options:', this.options);
    console.log(
      'MultipleSelectComponent - ngOnInit - selectedItems:',
      this.selectedItems
    );
    this.initializeMapOptions();
    this.updateSelectedOptions();
  }

  initializeMapOptions() {
    if (Array.isArray(this.options)) {
      this.mapOptions = this.options.map((item) => ({
        ...item,
        selected: false,
      }));

      // Cập nhật selectedItems nếu đã có dữ liệu
      if (this.selectedItems && this.selectedItems.length !== 0) {
        this.mapOptions = this.mapOptions.map((item) => ({
          ...item,
          selected: this.selectedItems.some(
            (selected) => selected.id === item.id
          ),
        }));

        this.updateLabel();
      }
    } else {
      console.error('Options is not an array:', this.options);
      this.mapOptions = [];
    }

    this.updateLabel();
  }

  updateSelectedOptions() {
    if (Array.isArray(this.options) && Array.isArray(this.selectedItems)) {
      this.mapOptions = this.mapOptions.map((item) => ({
        ...item,
        selected: this.selectedItems.some(
          (selected) => selected.id === item.id
        ),
      }));
      this.updateLabel();
    }
  }

  updateLabel() {
    if (this.selectedItems.length > 0) {
      this.label = this.selectedItems.map((item) => item.name).join(', ');
    } else {
      this.label = 'Lựa chọn tổ chức đồng hành!';
    }
  }

  isClicked: boolean = false;
  showOptions: boolean = false;

  labelClick() {
    this.isClicked = !this.isClicked;
    this.showOptions = !this.showOptions;
  }

  optionItemClick(option: any) {
    const index = this.selectedItems.findIndex((item) => item.id === option.id);
    if (index === -1) {
      this.selectedItems.push(option);
    } else {
      this.selectedItems.splice(index, 1);
    }

    // Update label based on selected items
    this.updateLabel();

    // Update mapOptions to ensure selected state is correct
    this.mapOptions = this.mapOptions.map((item) => ({
      ...item,
      selected: this.selectedItems.some((selected) => selected.id === item.id),
    }));

    // Emit the updated selectedItems array
    this.selectedItemsChange.emit(this.selectedItems);
  }
}
