import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

interface MoodEntry {
  id: number;
  createdAtUtc: string;
  mood: number;
  note?: string;
}

@Component({
  selector: 'app-mood-tracker',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mood-tracker.component.html',
  styleUrl: './mood-tracker.component.scss'
})
export class MoodTrackerComponent implements OnInit {
  private apiBaseUrl = '/api';

  moodEntries: MoodEntry[] = [];
  newMood = 5;
  newNote = '';
  isLoading = false;
  errorMessage = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadMoods();
  }

  loadMoods(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.http.get<MoodEntry[]>(`${this.apiBaseUrl}/mood`)
      .subscribe({
        next: entries => {
          this.moodEntries = entries;
          this.isLoading = false;
        },
        error: _ => {
          this.errorMessage = 'Failed to load mood history.';
          this.isLoading = false;
        }
      });
  }

  addMood(): void {
    this.errorMessage = '';

    const payload = {
      mood: this.newMood,
      note: this.newNote
    };

    this.http.post<MoodEntry>(`${this.apiBaseUrl}/mood`, payload)
      .subscribe({
        next: _ => {
          this.newMood = 5;
          this.newNote = '';
          this.loadMoods();
        },
        error: _ => {
          this.errorMessage = 'Failed to save mood entry.';
        }
      });
  }
}

