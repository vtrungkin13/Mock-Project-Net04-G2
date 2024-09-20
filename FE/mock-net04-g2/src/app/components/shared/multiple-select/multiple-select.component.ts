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

@Component({
  selector: 'app-multiple-select',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './multiple-select.component.html',
  styleUrl: './multiple-select.component.scss',
})
export class MultipleSelectComponent implements OnInit, OnChanges {
  @Input() label: string = '';
  @Input() options: any[] = [];
  @Output() onOptionSelect = new EventEmitter();

  mapOptions!: any[];
  @Input() selectedItems: any[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['options'] && !changes['options'].isFirstChange()) {
      this.initializeMapOptions();
    }
  }

  ngOnInit(): void {
    if (this.selectedItems.length !== 0) {
      this.label = this.selectedItems.map((item) => item.name).join(', ');

      this.mapOptions = this.mapOptions.map((item1) => {
        const match = this.selectedItems.find((item2) => item2.id === item1.id);
        if (match) {
          return {
            ...item1,
            selected: match.selected,
          };
        }
        return item1;
      });
      this.onOptionSelect.emit(this.selectedItems.map((item) => item.id));
    }
  }

  initializeMapOptions() {
    this.mapOptions = this.options.map((item) => {
      return { ...item, selected: false };
    });
  }

  isClicked: boolean = false;
  showOptions: boolean = false;

  labelClick() {
    this.isClicked = !this.isClicked;
    this.showOptions = !this.showOptions;
  }

  optionItemClick(option: any) {
    option.selected = !option.selected;

    if (option.selected) {
      this.selectedItems.push(option);
    } else {
      this.selectedItems = this.selectedItems.filter(
        (item) => item.id !== option.id
      );
    }

    // Update label based on selected items
    this.label = this.selectedItems.map((item) => item.name).join(', ');

    // Update mapOptions to ensure selected state is correct
    this.mapOptions = this.mapOptions.map((item1) => {
      const match = this.selectedItems.find((item2) => item2.id === item1.id);
      return {
        ...item1,
        selected: !!match, // true if found in selectedItems, false otherwise
      };
    });

    // Emit the ids of selected items
    this.onOptionSelect.emit(this.selectedItems.map((item) => item.id));
  }
}
