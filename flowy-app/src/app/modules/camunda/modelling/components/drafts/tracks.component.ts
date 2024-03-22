import { Component, Input, OnInit } from '@angular/core';
import { DraftTrack } from '../../models/draft-track.model';
import { CamundaModellingDraftsService } from '../../services/drafts.service';


@Component({
  selector: 'camunda-modelling-draft-tracks',
  template: `
    <div class="position-relative">
      <div *ngIf="isInLoading">Loading...</div>

      <table class="table table-sm table-hover table-striped">
        <thead>
          <tr>
            <th>eventAt</th>
            <th>userIdentifier</th>
            <th>operation</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let item of draftTracks">
            <td>{{item.eventAt}}</td>
            <td>{{item.userIdentifier}}</td>
            <td>{{item.operation}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})
export class CamundaModellingDraftTracksComponent implements OnInit {

  @Input() set idDraft(value: number) {
    this.loadData(value);
  }

  public isInLoading: boolean = false;
  public draftTracks?: Array<DraftTrack>;

  constructor(
    private draftsService: CamundaModellingDraftsService
  ) { }

  ngOnInit(): void { }

  loadData(value: number) : void{
    this.isInLoading = true;
    this.draftsService.GetDraftTracksByIdDraft(value).subscribe({
      next: (result) => {
        console.log(result);
        this.draftTracks = result;
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }
}
