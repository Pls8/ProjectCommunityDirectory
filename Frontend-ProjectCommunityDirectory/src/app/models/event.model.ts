export interface Event {
  id: number;
  title: string;
  description: string;
  eventDate: string;
  location: string;
  organizer: string;
  imagePath: string;
  categoryName: string; // used for display
  categoryId: number; //used when creating
}
